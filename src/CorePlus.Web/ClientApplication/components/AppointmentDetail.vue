<template>
  <div class="rounded overflow-hidden shadow-md mx-3 px-3 py-3 tracking-wide text-left">
    <p class="text-sm text-black-700 font-bold mb-3">{{ appointment.practitionerName }}</p>
    <p class="text-sm text-gray-700 mb-1 mt-1">DETAILS</p>
    <div class="">
      <p class="text-sm align-baseline mb-1">Type: {{ appointment.appointmentType }}</p>
      <div class="flex">
        <ClockIcon class="h-4 w-4 mr-2"/>
        <p class="text-sm align-baseline mb-1">{{ appointment.date }}</p>
      </div>
      <p class="text-sm align-baseline mb-1 ">Client: <span class="font-bold">{{ appointment.clientName }}</span></p>
      <p class="text-sm align-baseline mb-1">Duration: {{ appointment.duration }} minutes</p>
    </div>
    <p class="text-sm text-gray-700 mb-1 mt-1">PROFIT</p>
    <div class="flex space-x-4">
      <p class="text-sm align-baseline font-bold text-red-500">Cost: {{ appointment.cost }}</p>
      <p class="text-sm align-baseline font-bold text-green-500">Revenue: {{ appointment.revenue }}</p>
    </div>
  </div>
</template>

<script>
import {ClockIcon} from "@vue-hero-icons/outline";

export default {
  components: {ClockIcon},
  name: "AppointmentDetail",
  props: {
    appointment: {
      type: Object,
      required: true,
    },
  }
}
</script>

<style scoped>

</style>
