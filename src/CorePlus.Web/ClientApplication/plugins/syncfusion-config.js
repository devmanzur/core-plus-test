import Vue from "vue";
import { GridPlugin } from "@syncfusion/ej2-vue-grids";
import {
  DropDownListPlugin,
  MultiSelectPlugin,
} from "@syncfusion/ej2-vue-dropdowns";
import { DateRangePickerPlugin } from "@syncfusion/ej2-vue-calendars";
import { ChartPlugin } from '@syncfusion/ej2-vue-charts';

Vue.use(ChartPlugin);
Vue.use(DateRangePickerPlugin);
Vue.use(GridPlugin);
Vue.use(DropDownListPlugin);
Vue.use(MultiSelectPlugin);
