FROM ubuntu:focal

RUN apt-get update
RUN apt-get install -y wget python2

WORKDIR /nao

COPY nao.py .

RUN wget -qO- https://community-static.aldebaran.com/resources/2.8.6/pynaoqi-python2.7-2.8.6.23-linux64-20191127_152327.tar.gz | tar xz
RUN mv pynaoqi-python2.7-2.8.6.23-linux64-20191127_152327 pynaoqi-sdk

ENV PYTHONPATH=/nao/pynaoqi-sdk/lib/python2.7/site-packages
 
EXPOSE 25000 9559

CMD python2 nao.py
