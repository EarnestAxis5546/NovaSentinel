#!/bin/bash
sudo bpftool prog load ebpf/filter.o /sys/fs/bpf/nova_filter
sudo bpftool net attach xdp prog /sys/fs/bpf/nova_filter dev eth0