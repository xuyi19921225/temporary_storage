import { constantRoutes } from '@/router'

// 添加路由映射
const map = {
  layout: () => import('@/layout'),
  user: () => import('@/views/system/user/index'),
  role: () => import('@/views/system/role/index'),
  menu: () => import('@/views/system/menu/index'),
  vendor: () => import('@/views/system/vendor/index'),
  sapInvoice: () => import('@/views/invoice/sap/index'),
  siteInvoice: () => import('@/views/invoice/site/index'),
  matchReport: () => import('@/views/report/match-invoice/index'),
  unMatchReport: () => import('@/views/report/unmatch-invoice/index'),
  compareReport: () => import('@/views/report/compare-invoice/index'),
  payment: () => import('@/views/report/payment/index')
}

const state = {
  routes: [],
  asyncRoutes: []
}

const mutations = {
  SET_ROUTES: (state, asyncRoutes) => {
    state.asyncRoutes = asyncRoutes
    state.routes = constantRoutes.concat(asyncRoutes)
  }
}

const actions = {
  generateRoutes({ commit }, menuList) {
    return new Promise((resolve, reject) => {
      try {
        var asyncRoutes = []

        menuList.forEach(item => {
          item.component = map[item.component]
          item.children.forEach(children => {
            children.component = map[children.component]
          })
        })

        asyncRoutes = menuList
        asyncRoutes.push({ path: '*', redirect: '/404', hidden: true })
        commit('SET_ROUTES', asyncRoutes)
        resolve()
      } catch (error) {
        console.log(error)
        reject(error)
      }
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
