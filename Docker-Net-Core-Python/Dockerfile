FROM mcr.microsoft.com/dotnet/runtime:5.0-focal as base
RUN apt -y update
RUN apt -y install python3.9
RUN apt -y install python3-pip
RUN pip3 install Flask
WORKDIR /opt/flaskdemo
ADD app.py .
EXPOSE 5000
ENTRYPOINT ["python3", "./app.py"]
