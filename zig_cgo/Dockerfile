FROM ubuntu:latest
WORKDIR /usr/local/app

COPY . .

RUN /usr/local/app/ubuntu_setup.sh

CMD ["/usr/local/app/cross_compile.sh"]