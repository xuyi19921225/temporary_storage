import request from '@/utils/request'

export function getSiteAreaListByParameter(params) {
  return request({
    url: '/sitearea/GetSiteAreaListByParameter',
    method: 'get',
    params
  })
}

export function getSiteAreaPagingList(params) {
  return request({
    url: '/sitearea',
    method: 'get',
    params
  })
}

export function getSiteAreaList(params) {
  return request({
    url: '/sitearea/GetSiteAreaList',
    method: 'get',
    params
  })
}

export function addSiteAreaList(data) {
  return request({
    url: '/sitearea',
    method: 'post',
    data
  })
}

export function saveSiteAreaList(data) {
  return request({
    url: '/sitearea',
    method: 'put',
    data
  })
}

