import gradio as gr


def create_dynamic_outputs(num_outputs):
    output_components = []
    for i in range(int(num_outputs)):
        output_components.append(gr.Textbox(f"Output {i+1}"))
    return output_components


with gr.Blocks() as demo:
    num_slider = gr.Slider(minimum=1, maximum=5, step=1, label="Number of Outputs")

    # The @gr.render decorator makes this function re-run and render components
    @gr.render(inputs=[num_slider])
    def render_outputs(num_outputs):
        return create_dynamic_outputs(num_outputs)


demo.launch()
