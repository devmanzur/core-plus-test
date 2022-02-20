export const state = () => ({
  practitioners: [],
  reports: [],
  appointments: [],
});

export const mutations = {
  setPractitioners(state, content) {
    state.practitioners = content;
  },
  setReports(state, content) {
    state.reports = content;
  },
  setAppointments(state, content) {
    state.appointments = content;
  },
};

export const actions = {
  async getPractitioners({state, commit}, payload) {
    const existingItems = state.practitioners;
    if (existingItems.length > 0 && !payload.reload) return;
    const queryParams = {};

    const response = await this.$axios.$get('practitioners', {
      params: queryParams,
    });
    if (response.success) {
      commit('setPractitioners', response.data);
    }
  },

  async getReports({commit}, payload) {
    if (payload.practitioners == null || payload.practitioners.length === 0) {
      commit('setReports', []);
      return;
    }
    const queryParams = {
      practitioners: payload.practitioners.join(","),
      start: payload.dateRange[0],
      end: payload.dateRange[1]
    };
    const response = await this.$axios.$get('appointments/profit-reports', {
      params: queryParams,
    });
    if (response.success) {
      commit('setReports', response.data);
    }
  },

  async getAppointments({commit}, payload) {
    const queryParams = {
      practitionerId: payload.practitionerId,
      month: payload.month
    };
    const response = await this.$axios.$get('appointments', {
      params: queryParams,
    });
    if (response.success) {
      commit('setAppointments', response.data);
    }
  },
};
