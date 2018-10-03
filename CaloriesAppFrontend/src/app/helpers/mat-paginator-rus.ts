import {MatPaginatorIntl} from '@angular/material';
export class MatPaginatorRus extends MatPaginatorIntl {
  itemsPerPageLabel = 'Количество на странице';
  nextPageLabel     = 'Следующая страница';
  previousPageLabel = 'Предыдущая страница';
  firstPageLabel = 'Первая страница';
  lastPageLabel = 'Последняя страница';

  getRangeLabel = function (page, pageSize, length) {
    if (length === 0 || pageSize === 0) {
      return '0 от ' + length;
    }
    length = Math.max(length, 0);
    const startIndex = page * pageSize;
    const endIndex = startIndex < length ?
      Math.min(startIndex + pageSize, length) :
      startIndex + pageSize;
    return startIndex + 1 + ' - ' + endIndex + ' от ' + length;
  };
}
