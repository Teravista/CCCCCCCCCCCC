
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.8.3928.0
//  Copyright (c) Microsoft Corporation.  All rights reserved.



// Metadata version: v4.0.30319
.module extern ProgA.netmodule
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly extern System
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly extern JP_NET.Lab1a.O
{
  .ver 1:0:0:0
}
.assembly JP_NET.Lab5f
{
  .hash algorithm 0x00008004
  .ver 0:0:0:0
}
.file ClassA.netmodule
    .hash = (8B 66 B5 44 2A 99 BE 7B 8C FD 03 5A 12 E5 AD 5D   // .f.D*..{...Z...]
             B1 7D A6 B9 )                                     // .}..
.file ClassB.netmodule
    .hash = (89 33 DC D0 8C 70 0F 44 BC 4D 9B F3 C9 B6 D6 09   // .3...p.D.M......
             36 F0 82 9B )                                     // 6...
.file ProgA.netmodule
    .hash = (C3 CF AD 27 C8 94 77 B1 D9 FC 86 10 8B 77 E3 88   // ...'..w......w..
             51 C1 AB 9A )                                     // Q...
.class extern public JP_NET.Lab1.ClassA
{
  .file ClassA.netmodule
  .class 0x02000002
}
.class extern public JP_NET.Lab1.ClassB
{
  .file ClassB.netmodule
  .class 0x02000002
}
.module JP_NET.Lab5f.exe
// MVID: {5E2008FA-5505-47F2-81B6-2C12689BC770}
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00000001    //  ILONLY
// Image base: 0x06D30000


// ================== GLOBAL METHODS =========================

.method privatescope static void  __EntryPoint$PST06000001() cil managed
{
  .entrypoint
  // Code size       8 (0x8)
  .maxstack  8
  IL_0000:  tail.
  IL_0002:  call       void [.module ProgA.netmodule]JP_NET.Lab1.Program::Main()
  IL_0007:  ret
} // end of global method __EntryPoint


// =============================================================

// *********** DISASSEMBLY COMPLETE ***********************
// WARNING: Created Win32 resource file JP_NET.Lab5f.g.a.res
