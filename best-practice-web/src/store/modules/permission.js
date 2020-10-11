import { constantRoutes } from '@/router'
// import Layout from '@/layout'
// 添加路由映射
const map = {
  layout: () => import('@/layout'),
  user: () => import('@/views/permissions/user/index'),
  userCreate: () => import('@/views/permissions/user/create'),
  userEdit: () => import('@/views/permissions/user/edit'),
  projectMaintenance: () => import('@/views/maintenance/project-maintenance/index'),
  projectMaintenanceCreate: () => import('@/views/maintenance/project-maintenance/create'),
  projectMaintenanceEdit: () => import('@/views/maintenance/project-maintenance/edit'),
  mainMaintenance: () => import('@/views/maintenance/main-maintenance/index')
}

const state = {
  routes: [],
  asyncRoutes: []
}

const mutations = {
  SET_ROUTES: (state, routes) => {
    state.asyncRoutes = routes
    state.routes = constantRoutes.concat(routes)
  }
}

const actions = {
  generateRoutes({ commit }, menuList) {
    return new Promise(resolve => {
      try {
        var asyncRoutes = []
        if (menuList && menuList.length > 0) {
          var parentMenuList = menuList.filter((value) => value.parentID === -1)

          parentMenuList.forEach(parentElement => {
            var parentTemp = {
              path: parentElement.path,
              name: parentElement.name,
              component: map[parentElement.component],
              redirect: parentElement.redirect,
              alwaysShow: true,
              meta: {
                title: parentElement.title,
                icon: parentElement.icon
              }
            }

            var childrenMenuList = menuList.filter((value) => value.parentID === parentElement.id)
            var childrenTemp = []
            var temp = {}
            childrenMenuList.forEach(childrenElement => {
              temp = {
                path: childrenElement.path,
                name: childrenElement.name,
                component: map[childrenElement.component],
                hidden: childrenElement.hidden,
                meta: {
                  title: childrenElement.title,
                  icon: childrenElement.icon
                }
              }
              childrenTemp.push(temp)
            })

            parentTemp.children = childrenTemp

            asyncRoutes.push(parentTemp)
          })
        }
        asyncRoutes.push({ path: '*', redirect: '/404', hidden: true })
        commit('SET_ROUTES', asyncRoutes)
        resolve()
      } catch (error) {
        console.log(error)
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
