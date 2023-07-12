import classNames from "classnames/bind";

import styles from "./Pagination.module.scss";
import { usePagination } from "../../hooks";

const cx = classNames.bind(styles);

function Pagination({
	onPageChange,
	totalCount,
	totalPage = 0,
	siblingCount = 1,
	currentPage,
	pageSize = 10,
	className,
}) {
	const paginationRange = usePagination(totalCount, totalPage, pageSize, siblingCount, currentPage);
	const onPrevious = () => {
		onPageChange(currentPage - 1);
	};
	const onNext = () => {
		onPageChange(currentPage + 1);
	};
	if (paginationRange.length === 0) {
		return null;
	}
	const lastPage = paginationRange[paginationRange.length - 1];
	return (
		<ul className={cx(className, "Container")}>
			<li className={cx("Arrow", { disabled: currentPage === 1 })} onClick={onPrevious}>
				<i className="fa-solid fa-angle-left"></i>
			</li>
			{paginationRange.map((pagination) => {
				if (isNaN(Number(pagination))) {
					return (
						<li key={pagination} className={cx("Number", "dots")}>
							{"..."}
						</li>
					);
				}
				return (
					<li
						key={pagination}
						className={cx("Number", { active: currentPage === pagination })}
						onClick={() => {
							onPageChange(pagination);
						}}>
						{pagination}
					</li>
				);
			})}
			<li className={cx("Arrow", { disabled: currentPage === lastPage })} onClick={onNext}>
				<i className="fa-solid fa-angle-right"></i>
			</li>
		</ul>
	);
}

export default Pagination;
