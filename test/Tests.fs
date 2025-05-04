module Tests

open System
open Xunit
open Turing.Tape
open Turing.Instructions
open Turing.Machine
open Turing

let blankTape = intializeTape ""


[<Fact>]
let ``Initialize tape with empty string should work`` () =
    let tape = intializeTape ""
    Assert.Equal(blank, tape.Current)
    Assert.Empty(tape.Left)
    Assert.Empty(tape.Right)

[<Fact>]
let ``Write to blank tape should work`` () =
    let ttape = intializeTape ""

    
    let tapeWriter = write blankTape
    let tape = '1' |> tapeWriter
    Assert.Equal('1', tape.Current)
    Assert.Equal(1, tape |> toList |> List.length)
    Assert.Empty(tape.Left)
    Assert.Empty(tape.Right)

[<Fact>]
let ``Move left on blank tape should work`` () =    
    let tape = blankTape |> moveLeft    
    let actualList = toList tape

    Assert.Equal(2, List.length actualList)
    Assert.Collection(actualList, 
        (fun x -> Assert.Equal(blank, x)),
        (fun x -> Assert.Equal(blank, x)))
    Assert.Equal(blank, tape.Current)

[<Fact>]
let ``Move right on blank tape should work`` () =    
    let tape = blankTape |> moveRight
    let actualList = toList tape

    Assert.Equal(2, List.length actualList)
    Assert.Collection(actualList, 
        (fun x -> Assert.Equal(blank, x)),
        (fun x -> Assert.Equal(blank, x)))
    Assert.Equal(blank, tape.Current)

let tape = intializeTape "110"

let program = {
    states = 
        [ { name = "q0"; transitions = [ ('1', '1', "q0", Right); ('0', '0', "q1", Right) ] |> tupleToTransition }
          { name = "q1"; transitions = [ (blank, blank, "q2", Stay) ] |> tupleToTransition }
          { name = "q2"; transitions = [] } ]
    initialStateName = "q0"
    finalStateNames = ["q2"]
}


[<Fact>]
let ``One step should succeed`` () =
    newMachine program "110"
    |> function
    | Running m ->
        Assert.Equal("q0", m.currentState)
        Assert.Equal('1', m.tape.Current)
        
        doStep m
        |> function
        | Running m ->
            Assert.Equal("q0", m.currentState)
            Assert.Equal('1', m.tape.Current)
            Assert.Collection(m.tape |> toList, 
                (fun x -> Assert.Equal('1', x)),
                (fun x -> Assert.Equal('1', x)),
                (fun x -> Assert.Equal('0', x)))
        | _ -> Assert.True(false, "Expected Running state")
    | _ -> Assert.True(false, "Expected Running state")

let printer step machine =
    printfn $"Step {step} ({machine.currentState}): {machine.tape}"


[<Fact>]
let ``Multiple steps should succeed`` () =
    newMachine program "110"
    |> runMachine printer 0
    |> function
    | Finished m -> 
        Assert.Equal("q2", m.currentState)
        Assert.Equal(blank, m.tape.Current)
        Assert.Empty(m.tape.Right)
    | _ -> Assert.True(false, "Expected Finished state")


[<Fact>]
let ``Multiple steps should fail`` () =
    let rec runMachine steps machine =
        match machine with
        | Running m when List.contains m.currentState program.finalStateNames ->
            Assert.Fail("Expected to halt in final state")
            // Assert.Equal("q2", m.currentState)
            // Assert.Equal(blank, m.tape.Current)
            // Assert.Empty(m.tape.Right)
        | Running m ->
            printfn $"Step {steps}: {m.tape}"
            doStep m
            |> runMachine (steps + 1)
        | Halted m ->
            Assert.Equal("q1", m.currentState)
            Assert.Equal('1', m.tape.Current)
            Assert.Collection(m.tape |> toList, 
                (fun x -> Assert.Equal('1', x)),
                (fun x -> Assert.Equal('1', x)),
                (fun x -> Assert.Equal('0', x)),
                (fun x -> Assert.Equal('1', x)))
        | s -> Assert.True(false, $"Expected Running state {s}")
    
    newMachine program "1101"
    |> runMachine 0

let tellLike = 
    { states = 
        [ { name = "q0"; transitions = [
            ('1', 'x', "q0", Right) 
            ('0', 'x', "q0", Right)
            ('/', '/', "q1", Right) ] |> tupleToTransition }
          { name = "q1"; transitions = [
            ('1', '1', "q2", Left) 
            ('0', '0', "q2", Left)
            (blank, blank, "q5", Left) ] |> tupleToTransition }
          { name = "q2"; transitions = [
            ('x', 'x', "q2", Left) 
            ('/', '/', "q3", Left) ] |> tupleToTransition }
          { name = "q3"; transitions = [
            ('x', 'x', "q4", Right) ] |> tupleToTransition }
          { name = "q4"; transitions = [] } 
          { name = "q5"; transitions = [
            ('x', 'x', "q5", Left) 
            (blank, blank, "q4", Right) ] |> tupleToTransition } 
      ]
      initialStateName = "q0"
      finalStateNames = ["q4"]
    }

//[<Fact>]
let ``Multiple steps should succeed 2`` () =
    let rec runMachine steps machine =
        match machine with
        | Running m when List.contains m.currentState program.finalStateNames ->
            Assert.Equal("q4", m.currentState)
            Assert.Equal(blank, m.tape.Current)
            Assert.Empty(m.tape.Right)
        | Running m ->
            printfn $"Step {steps} ({m.currentState}): {m.tape}"
            doStep m
            |> runMachine (steps + 1)
        | _ -> Assert.True(false, "Expected Running state")
    
    newMachine tellLike "110/110"
    |> runMachine 0