FROM python:3.6.5
ADD ./ /
RUN pip install -r requirements.txt
RUN pip install rasa_nlu
RUN pip install rasa_core
CMD ["python", "server.py"]