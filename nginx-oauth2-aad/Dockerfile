FROM nginx:1.19.4
ADD ./misc/oauth2-proxy-v6.1.1.linux-amd64.tar.gz /opt
RUN cp /opt/oauth2-proxy-v6.1.1.linux-amd64/oauth2-proxy /usr/bin
RUN chmod +x /usr/bin/oauth2-proxy
COPY ./misc/oauth2-proxy-up /etc/
RUN chmod +x /etc/oauth2-proxy-up

ENTRYPOINT /etc/oauth2-proxy-up