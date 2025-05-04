module TuringPage

let template = """
<!DOCTYPE html>
<html lang="no">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Turing Machine</title>
    <link rel="stylesheet" href="/css/turing.css">
</head>
<body>
    <script src="/js/scripts.js"></script>
    <section id="turingRoot">
        <div id="turingMachine">
            <div id="tape"></div>
            <div id="controls">
                <button id="startButton">Start</button>
                <button id="stopButton">Stop</button>
                <button id="resetButton">Reset</button>
            </div>
            <div id="state"></div>
            <input type="textarea" id="turingInstructions" placeholder="Enter input here">
        </div>
    </section>
</body>
</html>
"""
let turingPage (context: Microsoft.AspNetCore.Http.HttpContext) =
    context.Response.ContentType <- "text/html"
    template