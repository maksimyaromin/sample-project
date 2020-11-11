export interface TableColumn {
    displayName: string;
    field: string;
    format: TableColumnFormat;
}

export enum TableColumnFormat {
    String = 'String',
    Number = 'Number',
    Date = 'Date',
    DateTime = 'DateTime',
    Currency = 'Currency'
}
