import request from '@/utils/request'

export function getMenusByRoleID(params) {
  return request({
    url: '/RoleMenu',
    method: 'get',
    params
  })
}

export function getMenus(params) {
  return request({
    url: '/menu',
    method: 'get',
    params
  })
}

export function getAllMenus(params) {
  return request({
    url: '/menu/getAllMenus',
    method: 'get',
    params
  })
}

export function addMenu(data) {
  return request({
    url: '/menu',
    method: 'post',
    data
  })
}

export function saveMenu(data) {
  return request({
    url: '/menu',
    method: 'put',
    data
  })
}

export function deleteMenu(params) {
  return request({
    url: '/menu',
    method: 'delete',
    params
  })
}
