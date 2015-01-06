# DarkMapleLib

Written by Yaminike.
https://wecodez.com/resources/darkmaplelib.43/

## Overview

I have had this laying around for quite a while.

To make it work for either EMS, GMS or KMS, just switch the build configuration over to either of those.
(Please note; KMS support only goes up to the latest version, approx 224)

It made to be a replacement for the MapleLib, as this version is much faster and improved on many points.

## Examples 

Below examples of how it can be used.

Creating a new Crypto instance:

```
ushort GameVersion = 157;
ulong AESKey = 0xA6807338CD343BE9;
CipherHelper OutCrypto = new CipherHelper(GameVersion, AESKey);
OutCrypto.PacketFinished += OutPacketFinished;
```

OutPacketFinished + ArrayReader:

```
private void OutPacketFinished(byte[] packet)
{
       ArrayReader reader = new ArrayReader(packet);
       ushort header = reader.ReadUShort();
}
```

How to convert the AESKey from MapleShark to 64bit
A6D4924F80CD9F5F73C63B6C380EA870CD27E77334FFC2033B133342E9CF9D7E
Where all colored numbers appended form the AESKey 0xA6807338CD343BE9

Feel free to request other examples.
And ofc questions are welcome.

*(Source licensed under the Apache License, Version 2.0) *