Browse For Speed 0.5

Configuration:
 - LFS Executable Path:
    The path to LFS.exe. Browse For Speed makes some attempt to find this on its first run
    but you may need to specify it manually.

 - Check for new version of Browse For Speed on start up
    When Browse for Speed loads, it can query our website to see if there is a new version
    available. If there is, a message box will show up prompting you to visit the download
    page. Only disabled by default as we don't approve of programs calling home
    automatically.

 - Start Before LFS
    A list of programs to be run before LFS.exe. This is useful for running mods and other
    utilities used with LFS. Name is purely for identification. Path is the executable to
    run and Arguments is the list of command line arguments to that executable.

 - Server double click behaviour
    When double clicking a server in the list view you can choose to have it join the
    server automatically or bring up the extended server information dialog.

 - Advanced
  - Query Wait:
    Windows XP SP2 introduced a limit of 10 outgoing TCP connections per second (this is 
    completely ridiculous). If your OS does not have this limit (XP SP2 definitely does have
    this limit), you may be able to check this box, and the whole query process will be 
    much, much faster. If you check this when you shouldn't, queries will start failing 
    after a random amount of succeeded queries.

    It has been reported that disabling query wait in Windows 2000 doesn't have any negative
    effects.

  - LFS InSim port
    LFS.exe can be started with /insim XXXX where XXXX is the port for InSim to listen on.
    This is necessary if you have some mods (such as Pit Spotter) enabled. Set it to 0 to
    disable.

Notes:

 - Track and ping filtering is only done client side. Choosing these before hitting refresh
   will have no effect.

 - Pubstat is used for extended server information and for the friends list. Pubstat queries
   are cached for 30 seconds, because 1) pubstat queries are rather large, even though
   Browse For Speed requests them gzipped and 2) pubstat allows a maximum of one query every
   5 seconds.
   
 - ctrl +/- increases/decreases font size in the lists.



http://www.browseforspeed.net

Comments/Abuse to: bfs@browseforspeed.net

Browse For Speed is Copyright 2006 Richard Nelson, Ben Kenny and Philip Nelson.

See CREDITS for a list of contributors.

Browse For Speed is provided without warranty.

Browse For Speed will always be free and open source.