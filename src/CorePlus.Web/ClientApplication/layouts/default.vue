<template>
  <div class="flex">
    <nav-bar style="position:fixed" :logo-url="logoUrl" :navigation="navigations">
    </nav-bar>
    <div class="container p-4">
      <nuxt class="pl-64"/>
    </div>
  </div>
</template>

<script>
import {CalendarIcon} from "@vue-hero-icons/outline";

export default {
  name: 'DefaultLayout',
  data() {
    return {
      logoUrl:
        'https://tailwindui.com/img/logos/workflow-logo-indigo-300-mark-white-text.svg',
      navigations: [
        {name: 'Appointments', icon: CalendarIcon, route: '/'}
      ],
    };
  },
  computed: {},
  mounted() {
  },
  methods: {}
};
</script>

<style>
@import 'https://cdn.syncfusion.com/ej2/tailwind.css';
</style>
