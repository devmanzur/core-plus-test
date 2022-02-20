<template>
  <div>
    <div>
      
    </div>
  </div>
</template>

<script>
import { LineSeries, Category, CandleSeries } from '@syncfusion/ej2-vue-charts';

export default {
  name: 'IndexPage',
  provide: {
    chart: [LineSeries, CandleSeries, Category],
  },
  data() {
    return {
     
    };
  },
};
</script>
<style>
#container {
  height: 350px;
}
</style>
