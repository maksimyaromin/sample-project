export class SearchQueryResult<T> {
    public currentPage: number;
    public total: number;

    public hasPreviousPage: boolean;
    public hasNextPage: boolean;

    public items: T[];

    constructor(init?: Partial<SearchQueryResult<T>>) {
        if (init) {
            Object.assign(this, init);
        }
    }
}
