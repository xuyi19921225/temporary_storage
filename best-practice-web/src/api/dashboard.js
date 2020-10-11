import request from '@/utils/request'

export function getProjectList(params) {
  return request({
    url: '/dashboard',
    method: 'get',
    params
  })
}
