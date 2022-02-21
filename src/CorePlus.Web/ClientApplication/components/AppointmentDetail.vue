<template>
  <div class="rounded overflow-hidden shadow-md mx-3 px-3 py-3 tracking-wide text-left">
    <p class="text-sm text-gray-700 mb-1">PRACTITIONER</p>
    <div class="flex space-x-4">
      <p class="text-sm align-baseline">Id: {{ appointment.practitionerId }}</p>
      <p class="text-sm align-baseline">Name: {{ appointment.practitionerName }}</p>
    </div>
    <p class="text-sm text-gray-700 mb-1 mt-1">DETAILS</p>
    <div class="">
      <p class="text-sm align-baseline mb-1">Date: {{ appointment.date }}</p>
      <p class="text-sm align-baseline mb-1">Type: {{ appointment.appointmentType }}</p>
      <p class="text-sm align-baseline mb-1">Client: {{ appointment.clientName }}</p>
      <p class="text-sm align-baseline mb-1">Duration: {{ appointment.duration }}</p>
    </div>
    <p class="text-sm text-gray-700 mb-1 mt-1">PROFIT</p>
    <div class="flex space-x-4">
      <p class="text-sm align-baseline text-red-400">Cost: {{ appointment.cost }}</p>
      <p class="text-sm align-baseline text-green-400">Revenue: {{ appointment.revenue }}</p>
    </div>
  </div>
</template>

<script>
export default {
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
