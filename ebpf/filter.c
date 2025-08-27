#include <linux/bpf.h>
#include <bpf/bpf_helpers.h>
#include <linux/if_ether.h>
#include <linux/ip.h>
#include <linux/tcp.h>
#include <linux/udp.h>

struct {
    __uint(type, BPF_MAP_TYPE_HASH);
    __uint(max_entries, 100000);
    __type(key, __u32); // IP address
    __type(value, __u64); // Request count
} rate_limit_map SEC(".maps");

#define RATE_LIMIT 35000 // Max requests per second
#define WINDOW 1000000000 // 1 second in nanoseconds

SEC("xdp")
int nova_filter(struct xdp_md *ctx)
{
    void *data = (void *)(long)ctx->data;
    void *data_end = (void *)(long)ctx->data_end;
    struct ethhdr *eth = data;

    if (data + sizeof(*eth) > data_end)
        return XDP_PASS;

    if (eth->h_proto != __constant_htons(ETH_P_IP))
        return XDP_PASS;

    struct iphdr *ip = data + sizeof(*eth);
    if ((void *)ip + sizeof(*ip) > data_end)
        return XDP_PASS;

    __u32 src_ip = ip->saddr;
    __u64 now = bpf_ktime_get_ns();
    __u64 *count = bpf_map_lookup_elem(&rate_limit_map, &src_ip);

    if (count) {
        if (now - *count < WINDOW) {
            if (*count > RATE_LIMIT) {
                return XDP_DROP;
            }
            *count += 1;
        } else {
            *count = 1;
        }
    } else {
        bpf_map_update_elem(&rate_limit_map, &src_ip, &now, BPF_ANY);
    }

    return XDP_PASS;
}

char _license[] SEC("license") = "GPL";