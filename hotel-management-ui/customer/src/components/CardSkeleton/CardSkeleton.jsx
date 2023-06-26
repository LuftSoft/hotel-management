export default function CardSkeleton() {
	return (
		<div className="skeletons border rounded p-3 bg-white" style={{ height: 180 }}>
			<div className="d-flex">
				<div className="skeleton skeleton-text" style={{ width: "132px", height: "150px" }}></div>
				<div className="flex-grow-1 ms-3">
					<div className="skeleton skeleton-text"></div>
					<div className="skeleton skeleton-text"></div>
					<div className="skeleton skeleton-text skeleton-text__body"></div>
					<div className="skeleton skeleton-text skeleton-footer"></div>
				</div>
			</div>
		</div>
	);
}
