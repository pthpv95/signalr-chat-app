const state = () => ({
  isHideHeader: false,
  isLoading: false,
})

// getters
const getters = {
  loadingState: (state) => state.isLoading,
  headerState: (state) => state.isHideHeader,
}

// actions
const actions = {
  hideHeader({ commit }) {
    commit("hideHeader")
  },
  showHeader({ commit }) {
    commit("showHeader")
  },
  showLoading({ commit }) {
    commit("showLoading")
  },
  hideLoading({ commit }) {
    commit("hideLoading")
  },
}

// mutations
const mutations = {
  hideHeader(state) {
    state.isHideHeader = true
  },

  showHeader(state) {
    state.isHideHeader = false
  },
  showLoading(state) {
    state.isLoading = true
  },

  hideLoading(state) {
    state.isLoading = false
  },
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
}
