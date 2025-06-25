FROM python:alpine3.7 

RUN pip install flask
ADD app.py /
WORKDIR /
EXPOSE 5000
ENTRYPOINT ["python"]
CMD ["./app.py"]