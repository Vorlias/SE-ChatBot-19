"""
    ChatBot Team 19
     
    NLU Training Code
"""

# Imports for the file
from rasa_nlu.training_data import load_data
from rasa_nlu.model import Trainer
from rasa_nlu import config
import const

def train(data, config_file, model_dir):
    """
    Function to train the bot
    """
    training_data = load_data(data)
    configuration = config.load(config_file)
    trainer = Trainer(configuration)
    trainer.train(training_data)
    trainer.persist(model_dir, fixed_model_name=const.MODEL_NAME)
    print("Training complete.")
    return


train(const.TRAINING_DATA_FILE, const.CONFIG_FILE, const.NLU_DIRECTORY)

