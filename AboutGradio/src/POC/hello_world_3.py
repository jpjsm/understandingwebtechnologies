import gradio as gr


def greet(name, is_morning, temperature):
    salutation = "Good morning" if is_morning else "Good evening"
    greeting = f"{salutation} {name}. It is {temperature} degrees today"
    celsius = (temperature - 32) * 5 / 9
    return greeting, round(celsius, 2)


demo = gr.Interface(
    fn=greet,
    inputs=["text", "checkbox", gr.Slider(0, 100)],
    outputs=["text", "number"],
    flagging_mode="manual",  # Essential for displaying custom flagging options
    flagging_options=[
        "Save Correct",
        "Save Ambiguous",
        "Save Incorrect",
    ],  # This creates a button labeled "Save Query Input and Output"
)
demo.launch()
