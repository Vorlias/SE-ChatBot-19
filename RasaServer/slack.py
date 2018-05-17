from rasa_core.channels import HttpInputChannel
from rasa_core.channels.slack import SlackInput
from rasa_core.agent import Agent
from rasa_core.interpreter import RegexInterpreter
import const

# load your trained agent
agent = Agent.load("dialogue", interpreter=RegexInterpreter())

input_channel = SlackInput(
   slack_token="xoxb-351036068130-363820053621-RJbTEl4ekYSaPBZQNWFahyHX",  # this is the `bot_user_o_auth_access_token`
   slack_channel="botchan"  # the name of your channel to which the bot posts
)

agent.handle_channel(HttpInputChannel(5000, "/parse", input_channel))