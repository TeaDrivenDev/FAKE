﻿module CommandlineParams

open Fake

let printAllParams() = printfn "FAKE.exe [buildScript] [Target] Variable1=Value1 Variable2=Value2 ... "

let parseArgs cmdArgs = 
    let splitter = [| '=' |]
    let split (arg:string) = 
      let pos = arg.IndexOfAny splitter 
      [| arg.Substring(0, pos); arg.Substring(pos + 1, arg.Length - pos - 1) |]
    cmdArgs
    |> Seq.skip 1
    |> Seq.mapi (fun (i : int) (arg : string) -> 
           if arg.Contains "=" then 
               let s = split arg
               if s.[0] = "logfile" then addXmlListener s.[1]
               s.[0], s.[1]
           else if i = 0 then "target", arg
           else arg, "true")
    |> Seq.toList