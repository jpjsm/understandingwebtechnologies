import gradio as gr


def create_dynamic_outputs(request, answers, summary):
    output_components = []
    for i in range(int(num_outputs)):
        output_components.append(gr.Textbox(f"Output {i+1}"))
    return output_components


with gr.Blocks() as demo:
    name = gr.Textbox(label="Name")
    output = gr.Textbox(label="Output Box")
    greet_btn = gr.Button("Greet")
    greet_btn.click(fn=greet, inputs=name, outputs=output, api_name="greet")

demo.launch()
