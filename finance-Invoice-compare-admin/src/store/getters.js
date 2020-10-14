const getters = {
  sidebar: state => state.app.sidebar,
  size: state => state.app.size,
  device: state => state.app.device,
  visitedViews: state => state.tagsView.visitedViews,
  cachedViews: state => state.tagsView.cachedViews,
  token: state => state.user.token,
  avatar: state => state.user.avatar,
  name: state => state.user.name,
  siteID: state => state.user.siteID,
  divisionID: state => state.user.divisionID,
  department: state => state.user.department,
  permission_routes: state => state.permission.asyncRoutes,
  routes: state => state.permission.routes,
  errorLogs: state => state.errorLog.logs
}
export default getters
