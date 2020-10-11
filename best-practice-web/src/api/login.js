import request from '@/utils/request'

export function login(params) {
  return request({
    url: '/login',
    method: 'get',
    params
  })
}

export function getSiteList(params) {
  return request({
    url: '/login/getSiteList',
    method: 'get',
    params
  })
}
