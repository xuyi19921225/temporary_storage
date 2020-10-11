import request from '@/utils/request'

export function getProjectHistoryList(params) {
  return request({
    url: '/projecthistory',
    method: 'get',
    params
  })
}
