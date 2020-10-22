import request from '@/utils/request'

export function getMenus(params) {
  return request({
    url: '/menu',
    method: 'get',
    params
  })
}

export function getAllParentMenus(params) {
  return request({
    url: '/menu/getAllParentMenus',
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

export function getTreeMenus(params) {
  return request({
    url: '/roleMenu/getTreeMenus',
    method: 'get',
    params
  })
}

export function saveRoleMenu(data) {
  return request({
    url: '/roleMenu',
    method: 'post',
    data
  })
}

export function getRMenuByRoleId(params) {
  return request({
    url: '/roleMenu',
    method: 'get',
    params
  })
}

