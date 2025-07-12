// MeowLang++ Compiler/Interpreter in F# (.NET 9 Functional Version) V1.0.2

open System

type Token =
    | Meow      // ptr + 1
    | Mreow     // ptr - 1
    | Miau      // mem[ptr] += 1
    | Mlem      // mem[ptr] -= 1
    | Purr      // print as char
    | Grr       // print as number (>:3)
    | Zzz       // zero memory at ptr (>:3c)
    | Push      // push pointer (:3)
    | Pop       // pop pointer (:3c)
    | LoopStart // nya
    | LoopEnd   // nya~

// Tokenizer
let tokenize (input: string) =
    input.Split([| ' '; '\n'; '\r' |], StringSplitOptions.RemoveEmptyEntries)
    |> Array.choose (fun word ->
        match word with
        | "meow" -> Some Meow
        | "mreow" -> Some Mreow
        | "miau" -> Some Miau
        | "mlem" -> Some Mlem
        | "purr" -> Some Purr
        | ":3" -> Some Push
        | ":3c" -> Some Pop
        | ">:3" -> Some Grr
        | ">:3c" -> Some Zzz
        | "nya" -> Some LoopStart
        | "nya~" -> Some LoopEnd
        | _ when word.StartsWith("#") -> None // ignore comments
        | _ -> None)
    |> List.ofArray

// Functional state model
type State = {
    Memory: Map<int, byte>
    Pointer: int
    Stack: int list
}

let getCell state =
    state.Memory |> Map.tryFind state.Pointer |> Option.defaultValue 0uy

let setCell state value =
    { state with Memory = state.Memory.Add(state.Pointer, value) }

let rec run tokens state =
    let rec eval tokens state loopStack : State =
        match tokens with
        | [] -> state
        | token :: rest ->
            match token with
            | Meow -> eval rest { state with Pointer = state.Pointer + 1 } loopStack
            | Mreow -> eval rest { state with Pointer = state.Pointer - 1 } loopStack
            | Miau ->
                let v = getCell state
                eval rest (setCell state (v + 1uy)) loopStack
            | Mlem ->
                let v = getCell state
                eval rest (setCell state (v - 1uy)) loopStack
            | Purr ->
                let v = getCell state
                printf "%c" (char v)
                eval rest state loopStack
            | Grr ->
                printf "%d" (getCell state)
                eval rest state loopStack
            | Zzz -> eval rest (setCell state 0uy) loopStack
            | Push -> eval rest { state with Stack = state.Pointer :: state.Stack } loopStack
            | Pop ->
                match state.Stack with
                | top :: restStack -> eval rest { state with Pointer = top; Stack = restStack } loopStack
                | [] -> failwith ":3c used with empty stack"
            | LoopStart ->
                let loopBody, afterLoop = extractLoop rest [] 1
                let rec loopExec st =
                    if getCell st <> 0uy then
                        loopExec (eval loopBody st [])
                    else st
                let newState = loopExec state
                eval afterLoop newState loopStack
            | LoopEnd -> failwith "Unexpected 'nya~'"
    and extractLoop tokens acc depth =
        match tokens with
        | [] -> failwith "Unclosed loop"
        | LoopStart :: rest -> extractLoop rest (LoopStart :: acc) (depth + 1)
        | LoopEnd :: rest when depth = 1 -> (List.rev acc, rest)
        | LoopEnd :: rest -> extractLoop rest (LoopEnd :: acc) (depth - 1)
        | t :: rest -> extractLoop rest (t :: acc) depth

    let _ = eval tokens state []
    ()

// Entry point
[<EntryPoint>]
let main argv =
    if argv.Length = 0 then
        printfn "Usage: meowlang <source-file.meow>"
    else
        let path = argv.[0]
        if IO.File.Exists(path) then
            let source =
                IO.File.ReadLines(path)
                |> Seq.map (fun l -> l.Split('#').[0]) // strip comments we dont need those
                |> String.concat " "
            let tokens = tokenize source
            let initialState = { Memory = Map.empty; Pointer = 0; Stack = [] }
            run tokens initialState
        else
            printfn "File not found: %s" path
    0
