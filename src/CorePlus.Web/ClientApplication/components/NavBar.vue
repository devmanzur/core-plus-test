<template>
  <div id="appNavBar" class="pt-5 pb-4 h-screen w-64 bg-indigo-700">
    <section>
      <div class="flex items-center flex-shrink-0 px-4">
        <img class="h-8 w-auto" :src="logoUrl" alt="company logo"/>
      </div>
      <nav class="mt-5 flex-1 px-2 space-y-1" aria-label="Sidebar">
        <nuxt-link
          v-for="item in navigation"
          :key="item.name"
          :to="item.route"
          :class="[
            isCurrentNav(item)
              ? 'bg-indigo-800 text-white'
              : 'text-indigo-100 hover:bg-indigo-600 hover:bg-opacity-75',
            'group flex items-center px-2 py-2 text-sm font-medium rounded-md',
          ]"
          @click.native="toggleNavItem(item)"
        >
          <component
            :is="item.icon"
            :class="[
              isCurrentNav(item) ? 'text-white' : 'text-indigo-300',
              'mr-3 flex-shrink-0 h-6 w-6',
            ]"
            aria-hidden="true"
          />
          <span class="flex-1">
            {{ item.name }}
          </span>
        </nuxt-link>
      </nav>
    </section>
    <section>
      <div class="flex-shrink-0 flex border-t border-indigo-800 p-4">
        <a href="#" class="flex-shrink-0 w-full group block">
          <div class="flex items-center">
            <div>
              <img
                class="inline-block h-9 w-9 rounded-full"
                src="https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80"
                alt=""
              />
            </div>
            <div class="ml-3">
              <p class="text-sm font-medium text-white">Unauthorized user!</p>
              <button
                class="
                  text-xs
                  font-medium
                  text-indigo-200
                  group-hover:text-white"
                @click="onSignInClicked"
              >
                Sign in
              </button>
            </div>
          </div>
        </a>
      </div>
    </section>
  </div>
</template>

<script>
export default {
  props: {
    logoUrl: {
      type: String,
      required: true,
    },
    navigation: {
      type: Array,
      required: true,
    }
  },
  data() {
    return {
      currentRoute: '',
    };
  },
  computed: {

  },
  mounted() {
    this.currentRoute = this.$route.path;
  },
  methods: {
    toggleNavItem(item) {
      this.currentRoute = item.route;
    },
    isCurrentNav(item) {
      return this.currentRoute === item.route;
    },
    async onSignOutClicked() {
      //  doing nothing
    },
    async onSignInClicked() {
    }
  }
};
</script>

<style scoped>
</style>
