FROM python:3.6.5
ADD ./ /
# need to install 0.12.3, for some reason it'll install 0.11.5??
RUN pip install -r requirements.txt
RUN pip install sklearn_crfsuite
RUN pip install spacy
RUN pip install -Iv rasa_nlu==0.12.3
RUN pip install rasa_core
CMD ["python", "server.py"]