<template>
  <Transition name="fade in">
    <div
      v-if="showing"
      class="fixed z-10 inset-0 overflow-y-auto"
      aria-labelledby="modal-title"
      role="dialog"
      aria-modal="true"
      @keydown.esc="close"
    >
      <div
        class="
          flex
          items-end
          justify-center
          min-h-screen
          pt-4
          px-4
          pb-20
          text-center
          sm:block sm:p-0
        "
      >
        <div
          class="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity"
          aria-hidden="true"
        ></div>
        <span
          class="hidden sm:inline-block sm:align-middle sm:h-screen"
          aria-hidden="true"
          >&#8203;</span
        >
        <div
          class="
            inline-block
            align-bottom
            bg-white
            rounded-lg
            text-left
            overflow-hidden
            shadow-xl
            transform
            transition-all
            sm:my-8 sm:align-middle sm:max-w-lg sm:w-full
          "
        >
          <button
            aria-label="close"
            class="absolute top-0 right-0 text-xl text-gray-500 my-2 mx-4"
            @click.prevent="close"
          >
            ×
          </button>
          <slot />
        </div>
      </div>
    </div>
  </Transition>
</template>
 
<script>
export default {
  props: {
    showing: {
      required: true,
      type: Boolean,
    },
  },
  watch: {
    showing(value) {
      if (value) {
        return document.querySelector('body').classList.add('overflow-hidden');
      }

      document.querySelector('body').classList.remove('overflow-hidden');
    },
  },
  mounted() {
    // document.addEventListener("keydown", (e) => {
    //     if (e.keyCode == 27) {
    //         this.$emit('close');
    //     }
    // });
  },
  methods: {
    close() {
      this.$emit('close');
    },
  },
};
</script>