<template>
  <div class='container'>
    <section class='mb-5 flex space-x-2.5 w-1/3'>
        <ejs-multiselect
        id='multiselect' :data-source='practitioners'
        :fields="fields" mode="CheckBox"
        v-model="filter.practitioners"
        placeholder="Select practitioners">
        </ejs-multiselect>
        <ejs-daterangepicker
        placeholder="Select date range"
        format="yyyy-MM-dd"
        v-model="filter.dateRange">
        </ejs-daterangepicker>
        <button class="bg-indigo-500 hover:bg-indigo-700 text-white font-semibold py-1 px-4 rounded" @click="onFiltersApplied">
          Search
        </button>
    </section>

    <section>
      <ejs-grid
        ref='reportsGrid'
        :data-source='reports'
        :allow-paging='true'
        :page-settings='settings.pageSettings'
        :edit-settings='settings.editSettings'
        :allow-sorting='true'
        :command-click='onCommandClicked'
      >
        <e-columns>
          <e-column
            field='practitionerId'
            header-text='Practitioner Id'
            :allow-editing='false'
            text-align='Left'
          ></e-column>
          <e-column
            field='totalRevenue'
            header-text='Total revenue'
            text-align='Center'
            :allow-editing='false'
          ></e-column>
          <e-column
            field='totalCost'
            header-text='Total cost'
            text-align='Center'
            :allow-editing='false'
          ></e-column>
          <e-column
            header-text='Actions'
            width='240'
            text-align='Center'
            :commands='settings.commands'
          ></e-column>
        </e-columns>
      </ejs-grid>
    </section>
    <section v-if='showAppointmentBreakDown' class='mt-8'>
      <p>Report has : {{ appointments.length }} appointments</p>
    </section>
  </div>
</template>

<script>
import {
  Page,
  Edit,
  Toolbar,
  Sort,
  CommandColumn
} from '@syncfusion/ej2-vue-grids';
import {MultiSelect, CheckBoxSelection} from "@syncfusion/ej2-vue-dropdowns";
import {cloneDeep} from 'lodash-es'
MultiSelect.Inject(CheckBoxSelection);

export default {
  name: 'UsersPage',
  provide: {
    grid: [Page, Edit, Toolbar, Sort, CommandColumn]
  },
  data() {
    return {
      fields: { text: 'name', value: 'id' },
      showAppointmentBreakDown: false,
      selectedReport: {
        month: '',
        practitionerId: 0
      },
      filter: {
        practitioners: [],
        dateRange: null,
      },
      settings: {
        gridSettings: {
          scrollSettings: {width: 886, height: 300},
          allowScrolling: true
        },
        editSettings: {
          allowEditing: false,
          allowAdding: false,
          allowDeleting: false,
        },
        commands: [
          {
            type: 'view-breakdown',
            title: 'View breakdown',
            buttonOption: {
              cssClass: 'e-flat',
              iconCss: 'e-lock e-icons'
            }
          }
        ],
        pageSettings: {pageSize: 20}
      }
    };
  },
  computed: {
    practitioners() {
      return cloneDeep(this.$store.state.appointments.practitioners);
    },
    reports() {
      return cloneDeep(this.$store.state.appointments.reports);
    },
    appointments() {
      return cloneDeep(this.$store.state.appointments.appointments);
    },
  },
  mounted() {
    this.getPractitioners();
  },
  methods: {
    onCommandClicked(args) {
      const type = args.commandColumn.type;
      if (type === 'view-breakdown') {
        const report = args.rowData;
        if (report.practitionerId === this.selectedReport.practitionerId
          && report.month === this.selectedReport.month
          && this.appointments.length > 0) {
          this.toggleBreakdownUI();
          return;
        }
        this.selectedReport = report;
        this.getAppointments();
        this.showAppointmentBreakDown = true;
      }
    },
    onFiltersApplied() {
      this.getReports();
    },
    getPractitioners(reload = false) {
      this.$store.dispatch('appointments/getPractitioners', {reload});
    },
    getAppointments() {
      this.$store.dispatch('appointments/getAppointments', this.selectedReport);
    },
    getReports() {
      debugger;
      this.$store.dispatch('appointments/getReports', this.filter);
    },
    toggleBreakdownUI() {
      this.showAppointmentBreakDown =
        !this.showAppointmentBreakDown;
    },
  }
};
</script>

<style>
</style>
