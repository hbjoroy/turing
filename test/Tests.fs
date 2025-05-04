module Tests

open Xunit
open Turing.Tape
open Turing.Instructions
open Turing.Machine
open Turing

let blankTape = intializeTape ""

[<Fact>]
let ``Initialize tape with empty string should work`` () =
    let tape = intializeTape ""
    Assert.Equal(BLANK, tape.Current)
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
        (fun x -> Assert.Equal(BLANK, x)),
        (fun x -> Assert.Equal(BLANK, x)))
    Assert.Equal(BLANK, tape.Current)

[<Fact>]
let ``Move right on blank tape should work`` () =    
    let tape = blankTape |> moveRight
    let actualList = toList tape

    Assert.Equal(2, List.length actualList)
    Assert.Collection(actualList, 
        (fun x -> Assert.Equal(BLANK, x)),
        (fun x -> Assert.Equal(BLANK, x)))
    Assert.Equal(BLANK, tape.Current)

let tape = intializeTape "110"

let program = {
    states = 
        [ { name = "q0"; transitions = [ ('1', '1', "q0", Right); ('0', '0', "q1", Right) ] |> toTransitions }
          { name = "q1"; transitions = [ (BLANK, BLANK, "q2", Stay) ] |> toTransitions }
          { name = "q2"; transitions = [] } ]
    initialStateName = "q0"
    finalStateNames = ["q2"]
}

let printer step machine =
    printfn $"Step {step} ({machine.currentState}): {machine.tape}"

[<Fact>]
let ``One step should succeed`` () =
    newMachine program "110"
    |> runMachine printer 0
    |> function
    | Finished m ->
        Assert.Equal("q2", m.currentState)
        Assert.Equal(BLANK, m.tape.Current)
        Assert.Collection(m.tape |> toList, 
            (fun x -> Assert.Equal('1', x)),
            (fun x -> Assert.Equal('1', x)),
            (fun x -> Assert.Equal('0', x)),
            (fun x -> Assert.Equal(BLANK, x)))
    | _ -> Assert.True(false, "Expected Finished state")

[<Fact>]
let ``Multiple steps should succeed`` () =
    newMachine program "110"
    |> runMachine printer 0
    |> function
    | Finished m -> 
        Assert.Equal("q2", m.currentState)
        Assert.Equal(BLANK, m.tape.Current)
        Assert.Empty(m.tape.Right)
    | _ -> Assert.True(false, "Expected Finished state")


[<Fact>]
let ``Multiple steps should halt`` () =
    newMachine program "1101"
    |> runMachine printer 0
    |> function
    | Halted m -> 
        Assert.Equal("q1", m.currentState)
        Assert.Equal('1', m.tape.Current)
        Assert.Empty(m.tape.Right)
    | _ -> Assert.True(false, "Expected Halted state")

let tellLike = 
    { states = 
        [ { name = "q0"; transitions = [
            ('1', 'x', "q0", Right) 
            ('0', 'x', "q0", Right)
            ('/', '/', "q1", Right) ] |> toTransitions }
          { name = "q1"; transitions = [
            ('1', '1', "q2", Left) 
            ('0', '0', "q2", Left)
            (BLANK, BLANK, "q5", Left) ] |> toTransitions }
          { name = "q2"; transitions = [
            ('x', 'x', "q2", Left) 
            ('/', '/', "q3", Left) ] |> toTransitions }
          { name = "q3"; transitions = [
            ('x', 'x', "q4", Right) ] |> toTransitions }
          { name = "q4"; transitions = [] } 
          { name = "q5"; transitions = [
            ('x', 'x', "q5", Left) 
            (BLANK, BLANK, "q4", Right) ] |> toTransitions } 
      ]
      initialStateName = "q0"
      finalStateNames = ["q4"]
    }

//[<Fact>] // This test is not working as expected, need to fix the instructions
let ``Multiple steps should succeed 2`` () =
    newMachine tellLike "110/110"
    |> runMachine printer 0
    |> function
    | Finished m -> 
        Assert.Equal("q4", m.currentState)
        Assert.Equal(BLANK, m.tape.Current)
        Assert.Empty(m.tape.Right)
    | _ -> Assert.True(false, "Expected Running state")
