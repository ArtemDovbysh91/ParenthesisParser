namespace ParenthesisParser.Application.FS

open System.Collections.Generic

module FSValidator =
    let bracketsMap =
        Map [
            '{', '}'
            '(',  ')'
            '[',  ']'
            '<',  '>'
        ]

    let validate input =
        let openBrackets = new Stack<char>()
        let mutable result = -1
        let indexedInput = seq { for i in 0 .. String.length input - 1 -> (i, input.[i]) }

        for (i, bracket) in indexedInput do
            if result > -1 then () // do nothing if we already have result
            elif bracketsMap.ContainsKey(bracket) then openBrackets.Push(bracket)
            elif openBrackets.Count = 0 then result <- i
            elif not (bracketsMap.[openBrackets.Pop()] = bracket) then result <- i

        result 
        
    let validatePipe (input: string) =
        let folder (openBrackets: Stack<char>, errorIndex: int) (index, bracket) = 
            let newErrorIndex =
                if bracketsMap.ContainsKey(bracket) then 
                    openBrackets.Push(bracket)
                    errorIndex
                elif errorIndex > -1 then errorIndex
                elif openBrackets.Count = 0 then index
                elif bracketsMap.[openBrackets.Pop()] <> bracket then index  
                else errorIndex
            
            (openBrackets, newErrorIndex)
            
        let seqIndexed input = seq { for i in 0 .. String.length input - 1 -> (i, input.[i]) }
           
        input
        |> seqIndexed 
        |> Seq.scan folder (new Stack<char>(), -1) 
        |> Seq.map (fun (stack, errorIndex) -> errorIndex)
        |> Seq.find (fun errorIndex -> errorIndex > -1) 


    let validateWithHeadAndTail (input: string) =
        let folder (openBrackets: char list, errorIndex: int) (index, bracket) = 
            if bracketsMap.ContainsKey(bracket) then bracket :: openBrackets, errorIndex
            else 
                match openBrackets, errorIndex with
                | _, errorIndex when errorIndex > -1 -> openBrackets, errorIndex
                | [], _ -> openBrackets, index 
                | head :: tail, _ when bracketsMap.[head] <> bracket -> openBrackets, index 
                | head :: tail, _ -> tail, errorIndex 
            
        // Seq.indexed is not available for some reason    
        let seqIndexed input = seq { for i in 0 .. String.length input - 1 -> (i, input.[i]) }
           
        input
        |> seqIndexed // seq { (0, "a"); (1, "b"); (2, "c") }
        |> Seq.scan folder ([], -1) 
        |> Seq.map (fun (stack, errorIndex) -> errorIndex)
        |> Seq.find (fun errorIndex -> errorIndex > -1) // It will find 1st error and stop

    let exists (x : int option) =
        match x with
        | Some(x) -> true
        | None -> false