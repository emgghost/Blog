<template>
  <div>
    <quill-editor
      v-model:modelValue="content"
      :options="editorConfig"
      @text-change="onTextChange"
      ref="quillRef"
    />
  </div>
</template>

<script>
import { ref, watch, onMounted, nextTick } from "vue";
import { QuillEditor } from "@vueup/vue-quill";
import "@vueup/vue-quill/dist/vue-quill.snow.css";

export default {
  components: {
    QuillEditor,
  },
  props: {
    modelValue: {
      type: String,
      default: "",
    },
  },
  setup(props, { emit }) {
    const content = ref(props.modelValue);
    const quillRef = ref(null); // Reference to Quill editor component

    const editorConfig = {
      theme: "snow",
      modules: {
        toolbar: [
          [{ header: [1, 2, false] }],
          ["bold", "italic", "underline", "strike"],
          [{ align: [] }],
          [{ list: "ordered" }, { list: "bullet" }],
          ["link", "image"],
          ["clean"],
        ],
      },
    };

    // Ensure content updates when parent prop changes
    watch(
      () => props.modelValue,
      async (newValue) => {
        if (newValue !== content.value) {
          content.value = newValue;

          // Wait for Quill editor to be ready
          await nextTick();

          if (quillRef.value && quillRef.value.getQuill) {
            const editor = quillRef.value.getQuill();
            if (editor.root.innerHTML !== newValue) {
              editor.root.innerHTML = newValue; // Force update
            }
          }
        }
      },
      { immediate: true } // Trigger on component mount
    );

    // Ensure content updates when user types
    watch(content, (newValue) => {
      emit("update:modelValue", newValue);
    });

    // Handle Quill text change event safely
    const onTextChange = () => {
      if (quillRef.value && quillRef.value.getQuill) {
        const editor = quillRef.value.getQuill(); // Get Quill instance
        content.value = editor.root.innerHTML; // Get updated content
      } else {
        console.warn("Quill editor is not initialized yet.");
      }
    };

    // Wait for the component to mount before using Quill instance
    onMounted(() => {
      if (!quillRef.value || !quillRef.value.getQuill) {
        console.warn("Quill editor is not ready on mount.");
      }
    });

    return {
      content,
      editorConfig,
      onTextChange,
      quillRef,
    };
  },
};
</script>
