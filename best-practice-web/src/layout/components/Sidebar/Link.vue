<template>
  <component :is="type" v-bind="linkProps(to)" @click.native="clickLink(to)">
    <slot />
  </component>
</template>

<script>
import { isExternal } from '@/utils/validate'

export default {
  props: {
    to: {
      type: String,
      required: true
    }
  },
  computed: {
    isExternal() {
      return isExternal(this.to)
    },
    type() {
      if (this.isExternal) {
        return 'a'
      }
      return 'router-link'
    }
  },
  methods: {
    linkProps(to) {
      if (this.isExternal) {
        return {
          href: to,
          target: '_blank',
          rel: 'noopener'
        }
      }
      return {
        to: to
      }
    },
    clickLink(to) {
      const fullPath = encodeURI(to)
      if (fullPath === this.$route.path) {
        this.$router.replace({
          path: '/redirect' + fullPath
        })
      } else {
        this.$router.push({
          path: fullPath
        })
      }
    }
  }
}
</script>
