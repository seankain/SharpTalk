Fork of SharpTalk to attempt to run a TTS service in Unity games.

The following list of phonemes and DECTalk documentation was copied from http://chrisnestrud.com/projects/dectalk

The table below lists the phonemic alphabet that DecTalk uses, along with an example of each sound. Some dictionaries put the stress symbol after the vowel nucleus or at the start of the syllable. However, DECtalk Software requires that the stress symbol appear immediately before the vowel.
Symbol	Word (showing phoneme)	Word (complete)
	silence	silence
aa	b ob	bob
ae	ba t	bat
ah	b ut	but
ao	bou ght	bought
aw	bout	bout
ax	a bout	about
ay	by te	byte
b	b ottle	bottle
ch	ch in	chin
d	d ebt	debt
dh	th is	this
dx	rid er	rider
eh	b et	bet
el	bottle	bottle
en	button	button
ey	ba ke	bake
f	f in	fin
g	guess	guess
hx	h ead	head
ih	b it	bit
ix	kisses	kisses
iy	bea t	beat
jh	g in	gin
k	Ken	Ken
l	l et	let
lx	el ectric	electric
m	m et	met
n	n et	net
nx	sing	sing
ow	boa t	boat
oy	b oy	boy
p	p et	pet
q	we e at	weeat
r	r ed	red
rr	b ird	bird
rx	or ation	oration
s	s it	sit
sh	sh in	shin
t	t est	test
th	th in	thin
tx	Lat in	Latin
uh	bo ok	book
uw	lu te	lute
v	v est	vest
w	w et	wet
yu	cu te	cute
yx	y et	yet
z	z oo	zoo
zh	az ure	azure

Each voice can be defined to create thousands of other voices. ":DV" stands for "Define Voice", and must be used before any of the defining commands. A space must be placed between ":DV" and the definition command. Following are some of the options which can be used to create your very own voices.

    AP = AVERAGE PITCH 50 - 350
    AS = ASSERTIVENESS 0 - 100
    BR = BREATHINESS 0 - 72
    GF = adjust volume of unvoiced sylables (d, f, g, h, j, p, s, t, part of v and z) (0-100, 0=voice without airflow, above 60 raises from default)
    GV = adjust nonbreathiness of sound (0-72)
    HS = HEAD SIZE 65 - 145
    LO = VOLUME 0 - 94
    PR = PITCH RANGE 0 - 250
    RA = SPEECH RATE 120 � 580 



SharpTalk
=========

A .NET wrapper for the FonixTalk TTS engine.


This project was inspired by the creative antics of those utilizing Moonbase Alpha's TTS feature. Aeiou.

I searched around exhausively for a decent TTS engine apart from Microsoft's SAPI, which has a .NET implementation. I don't like SAPI because its features are complicated, it depends on having custom voices installed, and SSML generally makes me want to puke.

Eventually, I came across DECtalk and its accompanying SDK, from which I was able to get documentation for its functions. I spent countless hours implementing these in C# using P/Invoke, and I eventually got it working.

After noticing some issues with DECtalk, I migrated the library to its successor, FonixTalk.


Features
-----
* Asynchronous speaking
* Phoneme events for mouth movements in games/animations
* Stream output for exporting voice audio as PCM data
* Sync() method makes it easy to synchronize voice output
* Adjustable voice and speaking rate
* Multiple engines can be independently controlled and talking at the same time
* Voices can be paused/resumed


How to use
------

Using the library is very simple and straightforward. Here is the basic code to make the engine speak:

```cs
var tts = new FonixTalkEngine();
tts.Speak("John Madden!");
```

You can easily change the voice of the engine, too! For example, to use the Frank voice, just add one line:

```cs
var tts = new FonixTalkEngine();
tts.Voice = TTSVoice.Frank;
tts.Speak("Here comes another Chinese earthquake! [brbrbrbrbrbrbrbrbrbrbrbrbrbrbrbrbrbrbr]");
```

Exporting speech audio to memory is just as simple:

```cs
var tts = new FonixTalkEngine();
byte[] audioBytes = tts.SpeakToMemory("Eat your heart out, SAPI!");
// Process audio data here
```

Or if you'd like a WAV file instead, SharpTalk has you covered.
```cs
var tts = new FonixTalkEngine();
tts.SpeakToWAVFile("speech.wav", "[:dv gv 0 br 120][:rate 300]I'm BatMan.");
```
