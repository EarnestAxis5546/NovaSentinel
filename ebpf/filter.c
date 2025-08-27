#include <linux/bpf.h>
#include <bpf/bpf_helpers.h>

SEC("xdp")
int nova_filter(struct xdp_md *ctx)
{
    // Placeholder eBPF program for packet filtering
    return XDP_PASS;
}

char _license[] SEC("license") = "GPL";