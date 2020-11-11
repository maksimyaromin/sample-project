export interface Sale {
    id: number;

    salesChannel?: string;
    shippedAt?: Date;

    unitsSold: number;
    unitPrice: number;
    unitCost: number;

    totalRevenue: number;
    totalCost: number;
    totalProfit: number;

    orderId: number;
    orderedAt: Date;
    orderPriorityId: number;
    orderPrioritySymbol: string;

    itemId: number;
    itemName: string;

    countryId: number;
    countryName: string;
    countryRegionId: number;
    countryRegionName: string;
}
