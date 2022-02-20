<template>
  <div class='container'>
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
import {Query} from '@syncfusion/ej2-data';
import {cloneDeep} from 'lodash-es'

export default {
  name: 'UsersPage',
  provide: {
    grid: [Page, Edit, Toolbar, Sort, CommandColumn]
  },
  data() {
    return {
      showAppointmentBreakDown: false,
      selectedReport: {
        month: '',
        practitionerId: 0
      },
      filter: {
        practitioners: [],
        dateRange: {},
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
