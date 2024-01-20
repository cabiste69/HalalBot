import discord, os
from random import randint

class MyClient(discord.Client):
    async def on_ready(self):
        print(f'Logged on as {self.user}!')

    async def on_message(self, message, isRef = False):
        if message.author == client.user:
            return
        if message.author.id in blockList:
            return
        if not isRef and not client.user.mentioned_in(message):
            return
        if MessageHasVideo(message):
            await self.GenerateHalalVideo(message)
            return
        if MessageHasImage(message):
            await self.GenerateHalalVideo(message, True)
            return

        if message.reference is not None:
            print("its a reef")
            msg = await message.channel.fetch_message(message.reference.message_id)
            await self.on_message(msg, True)
            return


    async def GenerateHalalVideo(self, message, isImage = False):
        await message.channel.send(f"adding nasheed to {message.attachments[0].filename}")
        media = message.attachments[0]
        nasheed = nasheeds[randint(0, len(nasheeds)-1)]
        if isImage:
            os.system(f'curl -L "{media.url}" -o "./output/{media.filename}"')
            media.url= f"./output/{media.filename}"
            command = f'ffmpeg -hide_banner -hwaccel mediacodec -y -r 10 -loop 1 -i "{media.url}" -i "{nasheedPath}/{nasheed}" -c:v h264_mediacodec -pix_fmt nv12 -tune stillimage -vf "fade=in:0:d=1, pad=ceil(iw/2)*2:ceil(ih/2)*2" -af "afade=in:0:d=1, silenceremove=1:0:-50dB" -b:a 128k -shortest -to 00:01:00 "./output/{media.filename}.mp4"'
        else:
            command = f'ffmpeg -hide_banner -hwaccel mediacodec -y -i "{media.url}" -i "{nasheedPath}/{nasheed}" -c:v h264_mediacodec -pix_fmt nv12 -map 0:v:0 -map 1:a:0 -af silenceremove=1:0:-50dB -b:a 128k -shortest "./output/{media.filename}.mp4"'
        print(f"executing [{command}]")
        os.system(command)
        file = discord.File(f'./output/{media.filename}.mp4')
        await message.reply(file=file)
        os.remove(f'./output/{media.filename}.mp4')

def MessageHasVideo(message):
    if not message.attachments:
        return False
    if not message.attachments[0].filename[-4:] in [".mp4", ".mov", "webm", ".gif"]:
        return False
    return True

def MessageHasImage(message):
    if not message.attachments:
        return False
    if not message.attachments[0].filename[-4:] in [".png", ".jpg", "jpeg"]:
        return False
    return True

def LoadNasheeds() -> list[str]:
    return os.listdir(nasheedPath)
#    with open(nasheedPath+"/all.txt", 'r') as f:
#        return f.readlines()

intents = discord.Intents.default()
intents.message_content = True

blockList = [1191341849747673131]
nasheedPath = "../../nasheed"
nasheeds = LoadNasheeds()
client = MyClient(intents=intents)
with open("token.txt", 'r') as f:
    token = f.readline()
client.run(token)
