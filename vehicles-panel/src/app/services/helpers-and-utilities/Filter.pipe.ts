import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

  transform(value: any, searchValue): any {
    if (!searchValue) return value;
    if (value[0].fullName != undefined && value[0].email) // filter users by fullName / email
      return value.filter((v) => v.fullName.toLowerCase().indexOf(searchValue.toLowerCase()) > -1 || v.email.toLowerCase().indexOf(searchValue.toLowerCase()) > -1 )

    else if (value[0].branch?.name != undefined && value[0].name != undefined) // filter department by Name / branch name
      return value.filter((v) => v.branch.name.toLowerCase().indexOf(searchValue.toLowerCase()) > -1 || v.name.toLowerCase().indexOf(searchValue.toLowerCase()) > -1 )

    else if (value[0].name != undefined) // filter branches by Name
      return value.filter((v) => v.name.toLowerCase().indexOf(searchValue.toLowerCase()) > -1)

  }
}
