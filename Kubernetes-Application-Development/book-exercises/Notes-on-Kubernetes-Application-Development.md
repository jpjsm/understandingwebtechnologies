# Notes on "Kubernetes Application Development"
Kubernetes is an operating system for a cluster of computers. This
operating system can be used for many workloads, but is particularly
suited for running robust, highly available, scaled web services. You can
think of containers like you think of processes in your computer's
operating system, where Kubernetes assumes the role of the OS. The key
difference is that containers let you encapsulate all of your application's
requirements into a single, reproducible unit. This is a powerful
abstraction for building better web applications.

## About PODS
Rather than working with containers
directly, Kubernetes adds a small
layer of abstraction called a pod. A
pod contains one or more containers,
and all the containers in a pod are guaranteed to run on the same
machine in the Kubernetes cluster. Containers in a pod share their
networking infrastructure, their storage resources, and their lifecycle.

