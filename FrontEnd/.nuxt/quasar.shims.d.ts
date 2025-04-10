type KeysMatching<T, V> = {
  [K in keyof T]-?: T[K] extends V ? K : never
}[keyof T] & keyof T

type OptionalKeys<T extends object> = Exclude<{
  [K in keyof T]: T extends Record<K, T[K]> ? never : K
}[keyof T], undefined>

type OmitMatching<T extends object, V> = Omit<T, KeysMatching<T, V>>

type OmitFnProps<T extends object> = OmitMatching<T, ((...args: any[]) => any) | undefined>

type PickOptionalProps<T extends object> = Pick<T, OptionalKeys<T>>

declare module 'nuxt-quasar-ui' {
  interface QuasarComponentDefaults {
    QAjaxBar?: PickOptionalProps<OmitFnProps<import("quasar").QAjaxBarProps>>
    QAvatar?: PickOptionalProps<OmitFnProps<import("quasar").QAvatarProps>>
    QBadge?: PickOptionalProps<OmitFnProps<import("quasar").QBadgeProps>>
    QBanner?: PickOptionalProps<OmitFnProps<import("quasar").QBannerProps>>
    QBar?: PickOptionalProps<OmitFnProps<import("quasar").QBarProps>>
    QBreadcrumbs?: PickOptionalProps<OmitFnProps<import("quasar").QBreadcrumbsProps>>
    QBreadcrumbsEl?: PickOptionalProps<OmitFnProps<import("quasar").QBreadcrumbsElProps>>
    QBtn?: PickOptionalProps<OmitFnProps<import("quasar").QBtnProps>>
    QBtnDropdown?: PickOptionalProps<OmitFnProps<import("quasar").QBtnDropdownProps>>
    QBtnGroup?: PickOptionalProps<OmitFnProps<import("quasar").QBtnGroupProps>>
    QBtnToggle?: PickOptionalProps<OmitFnProps<import("quasar").QBtnToggleProps>>
    QCard?: PickOptionalProps<OmitFnProps<import("quasar").QCardProps>>
    QCardActions?: PickOptionalProps<OmitFnProps<import("quasar").QCardActionsProps>>
    QCardSection?: PickOptionalProps<OmitFnProps<import("quasar").QCardSectionProps>>
    QCarousel?: PickOptionalProps<OmitFnProps<import("quasar").QCarouselProps>>
    QCarouselControl?: PickOptionalProps<OmitFnProps<import("quasar").QCarouselControlProps>>
    QCarouselSlide?: PickOptionalProps<OmitFnProps<import("quasar").QCarouselSlideProps>>
    QChatMessage?: PickOptionalProps<OmitFnProps<import("quasar").QChatMessageProps>>
    QCheckbox?: PickOptionalProps<OmitFnProps<import("quasar").QCheckboxProps>>
    QChip?: PickOptionalProps<OmitFnProps<import("quasar").QChipProps>>
    QCircularProgress?: PickOptionalProps<OmitFnProps<import("quasar").QCircularProgressProps>>
    QColor?: PickOptionalProps<OmitFnProps<import("quasar").QColorProps>>
    QDate?: PickOptionalProps<OmitFnProps<import("quasar").QDateProps>>
    QDialog?: PickOptionalProps<OmitFnProps<import("quasar").QDialogProps>>
    QDrawer?: PickOptionalProps<OmitFnProps<import("quasar").QDrawerProps>>
    QEditor?: PickOptionalProps<OmitFnProps<import("quasar").QEditorProps>>
    QExpansionItem?: PickOptionalProps<OmitFnProps<import("quasar").QExpansionItemProps>>
    QFab?: PickOptionalProps<OmitFnProps<import("quasar").QFabProps>>
    QFabAction?: PickOptionalProps<OmitFnProps<import("quasar").QFabActionProps>>
    QField?: PickOptionalProps<OmitFnProps<import("quasar").QFieldProps>>
    QFile?: PickOptionalProps<OmitFnProps<import("quasar").QFileProps>>
    QFooter?: PickOptionalProps<OmitFnProps<import("quasar").QFooterProps>>
    QForm?: PickOptionalProps<OmitFnProps<import("quasar").QFormProps>>
    QFormChildMixin?: PickOptionalProps<OmitFnProps<import("quasar").QFormChildMixinProps>>
    QHeader?: PickOptionalProps<OmitFnProps<import("quasar").QHeaderProps>>
    QIcon?: PickOptionalProps<OmitFnProps<import("quasar").QIconProps>>
    QImg?: PickOptionalProps<OmitFnProps<import("quasar").QImgProps>>
    QInfiniteScroll?: PickOptionalProps<OmitFnProps<import("quasar").QInfiniteScrollProps>>
    QInnerLoading?: PickOptionalProps<OmitFnProps<import("quasar").QInnerLoadingProps>>
    QInput?: PickOptionalProps<OmitFnProps<import("quasar").QInputProps>>
    QIntersection?: PickOptionalProps<OmitFnProps<import("quasar").QIntersectionProps>>
    QItem?: PickOptionalProps<OmitFnProps<import("quasar").QItemProps>>
    QItemLabel?: PickOptionalProps<OmitFnProps<import("quasar").QItemLabelProps>>
    QItemSection?: PickOptionalProps<OmitFnProps<import("quasar").QItemSectionProps>>
    QList?: PickOptionalProps<OmitFnProps<import("quasar").QListProps>>
    QKnob?: PickOptionalProps<OmitFnProps<import("quasar").QKnobProps>>
    QLayout?: PickOptionalProps<OmitFnProps<import("quasar").QLayoutProps>>
    QLinearProgress?: PickOptionalProps<OmitFnProps<import("quasar").QLinearProgressProps>>
    QMarkupTable?: PickOptionalProps<OmitFnProps<import("quasar").QMarkupTableProps>>
    QMenu?: PickOptionalProps<OmitFnProps<import("quasar").QMenuProps>>
    QNoSsr?: PickOptionalProps<OmitFnProps<import("quasar").QNoSsrProps>>
    QOptionGroup?: PickOptionalProps<OmitFnProps<import("quasar").QOptionGroupProps>>
    QPage?: PickOptionalProps<OmitFnProps<import("quasar").QPageProps>>
    QPageContainer?: PickOptionalProps<OmitFnProps<import("quasar").QPageContainerProps>>
    QPageScroller?: PickOptionalProps<OmitFnProps<import("quasar").QPageScrollerProps>>
    QPageSticky?: PickOptionalProps<OmitFnProps<import("quasar").QPageStickyProps>>
    QPagination?: PickOptionalProps<OmitFnProps<import("quasar").QPaginationProps>>
    QParallax?: PickOptionalProps<OmitFnProps<import("quasar").QParallaxProps>>
    QPopupEdit?: PickOptionalProps<OmitFnProps<import("quasar").QPopupEditProps>>
    QPopupProxy?: PickOptionalProps<OmitFnProps<import("quasar").QPopupProxyProps>>
    QPullToRefresh?: PickOptionalProps<OmitFnProps<import("quasar").QPullToRefreshProps>>
    QRadio?: PickOptionalProps<OmitFnProps<import("quasar").QRadioProps>>
    QRange?: PickOptionalProps<OmitFnProps<import("quasar").QRangeProps>>
    QRating?: PickOptionalProps<OmitFnProps<import("quasar").QRatingProps>>
    QResizeObserver?: PickOptionalProps<OmitFnProps<import("quasar").QResizeObserverProps>>
    QResponsive?: PickOptionalProps<OmitFnProps<import("quasar").QResponsiveProps>>
    QScrollArea?: PickOptionalProps<OmitFnProps<import("quasar").QScrollAreaProps>>
    QScrollObserver?: PickOptionalProps<OmitFnProps<import("quasar").QScrollObserverProps>>
    QSelect?: PickOptionalProps<OmitFnProps<import("quasar").QSelectProps>>
    QSeparator?: PickOptionalProps<OmitFnProps<import("quasar").QSeparatorProps>>
    QSkeleton?: PickOptionalProps<OmitFnProps<import("quasar").QSkeletonProps>>
    QSlideItem?: PickOptionalProps<OmitFnProps<import("quasar").QSlideItemProps>>
    QSlideTransition?: PickOptionalProps<OmitFnProps<import("quasar").QSlideTransitionProps>>
    QSlider?: PickOptionalProps<OmitFnProps<import("quasar").QSliderProps>>
    QSpace?: PickOptionalProps<OmitFnProps<import("quasar").QSpaceProps>>
    QSpinner?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerProps>>
    QSpinnerAudio?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerAudioProps>>
    QSpinnerBall?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerBallProps>>
    QSpinnerBars?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerBarsProps>>
    QSpinnerBox?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerBoxProps>>
    QSpinnerClock?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerClockProps>>
    QSpinnerComment?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerCommentProps>>
    QSpinnerCube?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerCubeProps>>
    QSpinnerDots?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerDotsProps>>
    QSpinnerFacebook?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerFacebookProps>>
    QSpinnerGears?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerGearsProps>>
    QSpinnerGrid?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerGridProps>>
    QSpinnerHearts?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerHeartsProps>>
    QSpinnerHourglass?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerHourglassProps>>
    QSpinnerInfinity?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerInfinityProps>>
    QSpinnerIos?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerIosProps>>
    QSpinnerOrbit?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerOrbitProps>>
    QSpinnerOval?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerOvalProps>>
    QSpinnerPie?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerPieProps>>
    QSpinnerPuff?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerPuffProps>>
    QSpinnerRadio?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerRadioProps>>
    QSpinnerRings?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerRingsProps>>
    QSpinnerTail?: PickOptionalProps<OmitFnProps<import("quasar").QSpinnerTailProps>>
    QSplitter?: PickOptionalProps<OmitFnProps<import("quasar").QSplitterProps>>
    QStep?: PickOptionalProps<OmitFnProps<import("quasar").QStepProps>>
    QStepper?: PickOptionalProps<OmitFnProps<import("quasar").QStepperProps>>
    QStepperNavigation?: PickOptionalProps<OmitFnProps<import("quasar").QStepperNavigationProps>>
    QTabPanel?: PickOptionalProps<OmitFnProps<import("quasar").QTabPanelProps>>
    QTabPanels?: PickOptionalProps<OmitFnProps<import("quasar").QTabPanelsProps>>
    QTable?: PickOptionalProps<OmitFnProps<import("quasar").QTableProps>>
    QTd?: PickOptionalProps<OmitFnProps<import("quasar").QTdProps>>
    QTh?: PickOptionalProps<OmitFnProps<import("quasar").QThProps>>
    QTr?: PickOptionalProps<OmitFnProps<import("quasar").QTrProps>>
    QRouteTab?: PickOptionalProps<OmitFnProps<import("quasar").QRouteTabProps>>
    QTab?: PickOptionalProps<OmitFnProps<import("quasar").QTabProps>>
    QTabs?: PickOptionalProps<OmitFnProps<import("quasar").QTabsProps>>
    QTime?: PickOptionalProps<OmitFnProps<import("quasar").QTimeProps>>
    QTimeline?: PickOptionalProps<OmitFnProps<import("quasar").QTimelineProps>>
    QTimelineEntry?: PickOptionalProps<OmitFnProps<import("quasar").QTimelineEntryProps>>
    QToggle?: PickOptionalProps<OmitFnProps<import("quasar").QToggleProps>>
    QToolbar?: PickOptionalProps<OmitFnProps<import("quasar").QToolbarProps>>
    QToolbarTitle?: PickOptionalProps<OmitFnProps<import("quasar").QToolbarTitleProps>>
    QTooltip?: PickOptionalProps<OmitFnProps<import("quasar").QTooltipProps>>
    QTree?: PickOptionalProps<OmitFnProps<import("quasar").QTreeProps>>
    QUploader?: PickOptionalProps<OmitFnProps<import("quasar").QUploaderProps>>
    QUploaderAddTrigger?: PickOptionalProps<OmitFnProps<import("quasar").QUploaderAddTriggerProps>>
    QVideo?: PickOptionalProps<OmitFnProps<import("quasar").QVideoProps>>
    QVirtualScroll?: PickOptionalProps<OmitFnProps<import("quasar").QVirtualScrollProps>>
  }
}

declare module '@nuxt/schema' {
  interface AppConfigInput {
    ["nuxtQuasar"]?: import("nuxt-quasar-ui").QuasarUIConfiguration
  }
  interface AppConfig {
    ["nuxtQuasar"]?: import("nuxt-quasar-ui").QuasarUIConfiguration
  }
}

export {}
