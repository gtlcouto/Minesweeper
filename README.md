# Minesweeper
Minesweeper on unity + server + redis + rest + db

INSTRUCTION: 

1. Run Sockpol.exe from cmd. "Sockpol.exe --all"
2. Run Server from server and clientdll
3. Open Unity, open project on folder the folder unity_files
4. Open LinkSyncSCR Script and change the IP Address to your IP Address.
5 Run Unity project -> will connect to facebook -> connects to the server via SharpConnect.dll(clientdll project)/LinkSyncSCR(unity script)

TO-DO

1- Send game logic to the server. 
2- Server generates mines and assigns it to the facebook id of the user connecting.
3- Client clicks or flags a mine field and send the position and actiontype to the server.
4- Server checks result of click and sends either (game over + mine positions to client / list of revealed nodes )
5- Client receives and updates the board - > back to #3 till games over

-Message Length prefixing

-Save some sort of leaderboard on mysql ( either time to complete or keep reseting the board adding more and more mines)
-view leaderboard buttons which queries mysql db  via http get
-redis?

Hosting is done on my dropbox and facebook account.

Use user access token to login via facebook(just click the button)
