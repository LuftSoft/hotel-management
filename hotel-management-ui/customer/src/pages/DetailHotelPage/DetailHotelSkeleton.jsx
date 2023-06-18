export default function DetailHotelSkeleton() {
	return (
		<div className=" bg-light py-3">
			<div className="Container my-3 d-flex flex-column gap-3">
				<div className="bg-white rounded border">
					<div className="d-flex flex-column p-3">
						<div className="d-flex flex-column">
							<h4 className="skeleton skeleton-text"></h4>
							<div className="d-flesx align-items-center mb-2">
								<div className="d-flexs me-2">
									<div className="skeleton skeleton-text w-50"></div>
								</div>
								<div className="d-flex align-items-center"></div>
							</div>
							<div className="d-flex skeleton skeleton-text w-50">
								<div className="ms-2"></div>
							</div>
							<div className="border my-3"></div>
							<div className="d-flex">
								<div className="d-flex flex-grow-1 me-2 skeleton" style={{ width: 768, height: 488 }}></div>
								<div className="d-flex flex-column" style={{ width: 152 }}>
									<div className="skeleton skeleton-text h-25"></div>
									<div className="skeleton skeleton-text h-25"></div>
									<div className="skeleton skeleton-text h-25"></div>
									<div className="skeleton skeleton-text h-25"></div>
								</div>
							</div>
							<div className="d-flexs justify-content-end mt-2 skeleton skeleton-text"></div>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}
