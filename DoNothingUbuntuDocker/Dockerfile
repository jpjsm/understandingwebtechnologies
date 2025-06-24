FROM ubuntu:18.04
RUN apt-get update
RUN apt-get -y install apt-utils
RUN apt-get -y install net-tools
RUN apt-get -y install iproute2
RUN apt-get -y install iputils-ping
RUN apt-get -y install tcpdump
RUN apt-get -y install netcat
RUN apt-get -y install curl
WORKDIR /
ENTRYPOINT ["top", "-b"]
